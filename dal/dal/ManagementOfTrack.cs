﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common;
namespace dal
{
    public class ManagementOfTrack
    {
        public static ManagementOfTrack management_of_track;
        static ManagementOfTrack()
        {
            management_of_track = new dal.ManagementOfTrack();
        }
        
        public List<DetailsOfTrack> GetTracks()
        {
            List<Track_to_travel> tracks;
            using (var DbContext = new DataBaseEntities1())
            {
                tracks = DbContext.Track_to_travel.ToList();
            }
            return tracks.Select(t => Mapper.ConvertTrackToCommon(t)).ToList();
        }
        public List<DetailsOfTrack>GetTracks(int travelCode)
        {
            List<Track_to_travel> tracks;
            using (var DbContext=new DataBaseEntities1())
            {
                tracks = DbContext.Track_to_travel.Where(t => t.Travel_s_code == travelCode).ToList();
            }
            return tracks.Select(t => Mapper.ConvertTrackToCommon(t)).ToList();
        }
        public void AddTrack(DetailsOfTrack detailsOfTrack)
        {
            DataBaseEntities1 db = new DataBaseEntities1();
            db.Track_to_travel.Add(detailsOfTrack.ConvertTrackToDal());
            db.SaveChanges();
        }

        public void UpdateTrack(DetailsOfTrack detailsOfTrack)
        {
            Track_to_travel tracks = Mapper.ConvertTrackToDal(detailsOfTrack);
            using (var db = new DataBaseEntities1())
            {
                db.Entry<Track_to_travel>(db.Set<Track_to_travel>().Find(tracks.Track_s_code)).CurrentValues.SetValues(tracks);
                db.SaveChanges();
            }
        }
        public void RemoveTrack(string id)
        {
            using (var db = new DataBaseEntities1())
            {
                Track_to_travel t = db.Track_to_travel.Find(id);
                if (t != null)
                {
                    db.Track_to_travel.Remove(t);
                    db.SaveChanges();
                }
            }
        }
    }
}
