using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRESTSecurity.Models
{
    public class Supercar
    {
        public int SupercarId { get; set; }

        public string ThumbnailUri { get { return string.Format("http://hack-yourself-first.com/Images/Cars/{0}/thumb.jpg", SupercarId); } }

        public string PrimaryPicUri { get { return string.Format("http://hack-yourself-first.com/Images/Cars/{0}/1.jpg", SupercarId); } }

        public string Make { get; set; }

        public string Model { get; set; }

        public string MakeAndModel { get { return string.Format("{0} {1}", Make, Model); } }

        public int PowerKw { get; set; }

        public int TorqueNm { get; set; }

        public double ZeroToOneHundredKmInSecs { get; set; }

        public int TopSpeedKm { get; set; }

        public string VotesText { get { return string.Format("{0:n0} {1}", Votes.Count, Votes.Count.Equals(1) ? "vote" : "votes"); } }

        public string SpecsText { get { return string.Format("0-100kph in {0:n1} secs, {1:n0}kw, {2:n0}kph", ZeroToOneHundredKmInSecs, PowerKw, TopSpeedKm); } }

        public List<Vote> Votes { get; set; }

        public double WeightKg { get; set; }

        public string EngineLayout { get; set; }

        public int EngineCc { get; set; }
    }
}
