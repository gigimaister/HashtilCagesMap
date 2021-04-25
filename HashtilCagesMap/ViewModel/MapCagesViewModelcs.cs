using HashtilCagesMap.DataBase;
using HashtilCagesMap.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilCagesMap.ViewModel
{

    class MapCagesViewModelcs
    {

        DBManagerOld dbManagerOld = new DBManagerOld();
        private ObservableCollection<CageAudit> _cages;
        public ObservableCollection<CageAudit> Cages
        {
            get { return _cages; }
            set { _cages = value; }
        }

        public MapCagesViewModelcs(int i)
        {
            if (i == 0)
            {
                _cages = new ObservableCollection<CageAudit>();
                this.GenerateOrders();
            }
            else
            {
                _cages = new ObservableCollection<CageAudit>();
                this.GenerateOrdersPickUp();
            }
        }

        private async void GenerateOrdersPickUp()
        {
            _cages = await Task.Run(()=> dbManagerOld.SelectAllTable("cage_audit WHERE status='איסוף' ORDER BY date DESC, time DESC"));
        }

        private async  void GenerateOrders()
        {
            _cages = await Task.Run(()=> dbManagerOld.SelectAllTable("cage_audit ORDER BY date DESC, time DESC"));
        }
    }
}