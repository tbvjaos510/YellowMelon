using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using Dapper;
using YellowMelon.Model;

namespace YellowMelon.ViewModel
{
    public class ArtistViewModel
    {
        public Artist GetArtist(int artistIdx)
        {
            return DataAccess.db.Query<Artist, User, Artist>("select a.art_idx, a.art_nick, a.art_date, a.art_exp, " +
                "b.u_idx, b.u_id, b.u_name from artist_tb as a join user_tb as b on a.u_idx = b.u_idx where a.art_idx = " + artistIdx, (art, usr) =>
                {
                    art.FK_User = usr;
                    return art;
                }, splitOn:"u_idx").FirstOrDefault();
        }

        public List<Music> GetMusics(int artistIdx)
        {
            string query = "select a.mus_idx, a.mus_name, a.mus_exp, a.mus_like, a.mus_is_open, a.mus_link, " +
                "b.art_idx, b.u_idx, b.art_nick, b.art_date, b.art_exp, c.gn_idx, c.gn_name from music_tb as a join artist_tb as b on a.art_idx = b.art_idx join " +
                "genre_rf as c on a.gn_idx = c.gn_idx where b.art_idx = " + artistIdx;
            return DataAccess.db.Query<Music, Artist, Genre, Music>(query, (m, a, g) =>
            {
                m.FKGenre = g;
                m.FKArtist = a;
                return m;
            }, splitOn: "art_idx,gn_idx").ToList();
        }
    }
}
