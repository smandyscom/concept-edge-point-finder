using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using System.ComponentModel;

namespace Presentation.ViewModels
{
    class ViewModelGeneric<TModel> : INotifyPropertyChanged

    {

        private readonly TModel _dataObject;

        public ViewModelGeneric(TModel dataObject)

        {

            _dataObject = dataObject;

        }



        protected TModel DataObject { get { return _dataObject; } }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")

        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
