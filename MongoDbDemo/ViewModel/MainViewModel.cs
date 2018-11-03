using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.DataProcessing;
using DevExpress.Xpf.Core.ServerMode;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using MongoDbDemo.Models;
using MongoDbDemo.Repositories;

namespace MongoDbDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private IBooksRepository _booksRepository;
        private IQueryable<Book> _booksQueriable;

        public ObservableCollection<BookViewModel> Books { get; set; } = new ObservableCollection<BookViewModel>();

        public BookViewModel SelectedBook { get; set; }

        public LinqServerModeDataSource QueriableDataSource { get; set; }   

        public ICommand SaveCommand { get; set; }
        public ICommand LoadDataCommand { get; set; }

        public IQueryable<Book> BooksQueriable
        {
            get { return _booksQueriable; }
            private set
            {
                _booksQueriable = value; 
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                Books = new ObservableCollection<BookViewModel>();
                Books.Add(new BookViewModel() { Author = "asd", Name = "asda" });
            }

        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        [PreferredConstructor]
        public MainViewModel(IBooksRepository booksRepository) : this()
        {
            if (IsInDesignMode)
                return;
            
            _booksRepository = booksRepository;
            SaveCommand = new RelayCommand(SaveChanges);
            LoadDataCommand = new RelayCommand(LoadData);
            Books.CollectionChanged += (sender, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                    {
                        var book = item as BookViewModel;
                        if (book != null)
                            book.IsNew = true;
                    }
                }

            };
            BooksQueriable = _booksRepository.GetAllBooksAsQueryable();

            QueriableDataSource = new LinqServerModeDataSource()
            {
                QueryableSource = BooksQueriable

            };

        }

        public void SaveChanges()
        {
            var updates = Books.Where(b => b.HasChanges);
            var newBooks = Books.Where(b => b.IsNew).Select(b=>b.ToBook());

            _booksRepository.AddBooks(newBooks);
        }

        public void LoadData()
        {
            BooksQueriable = _booksRepository.GetAllBooksAsQueryable();
            Books.Clear();
            Books.AddRange(BooksQueriable.Where(a=>a.AvailableAmount > 100).ToList().Select(b => new BookViewModel(b)));

        }
    }
}