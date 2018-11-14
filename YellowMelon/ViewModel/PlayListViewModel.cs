using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using Dapper;
using System.Collections.ObjectModel;
using YellowMelon.Model;

namespace YellowMelon.ViewModel
{
    public class PlayListViewModel
    {
        public ObservableCollection<MList> PlayLists;
        public ObservableCollection<Music> defaultPlayList;

        public PlayListViewModel(User user)
        {
            List<MList> playlist = DataAccess.db.Query<MList>("select * from list_tb where u_idx=@id", new { id = user.Id }).ToList();
            foreach (MList list in playlist)
            {
                list.PlayList = new ObservableCollection<ListMusic>(
                // TODO: join된 쿼리문 및 Model 추가
                    );
            }
        }
    }
}
