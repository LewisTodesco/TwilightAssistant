using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Services
{
    public interface IGetSaveServices<T>
    {
        public ObservableCollection<T> GetOfflineData(string targetFile);
        public ObservableCollection<T> GetOnlineData();
        public void SaveOfflineData(ObservableCollection<T> collection_to_save, string targetFile);
        public ObservableCollection<T> SaveOnlineData();
    }
}
