using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MongoDbDemo.Models;

namespace MongoDbDemo.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        private string _name;
        private string _author;
        private string _id;

        public BookViewModel()
        {
            PropertyChanged += BookViewModel_PropertyChanged;
        }
        public BookViewModel(Book book) : this()
        {
            _id = book.Id;
            _author = book.Author;
            _name = book.Name;
        }

        public Book ToBook()
        {
            return new Book() {Author = this.Author, Id = this.Id, Name = this.Name};
        }

        private void BookViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HasChanges = true;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                    return;
                _name = value;
                RaisePropertyChanged();
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (value == _author)
                    return;
                _author = value;
                RaisePropertyChanged();
            }
        }

        public string Id
        {
            get { return _id; }
            set
            {
                if (value == _id)
                    return;
                _id = value;
                RaisePropertyChanged();
            }
        }

        public bool IsNew { get; set; }
        public bool HasChanges { get; set; }
    }
}
