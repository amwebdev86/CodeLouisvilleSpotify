using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLouSpotify.Models
{
   
    public class AudioAnalysis
    {
        public Bar[] bars { get; set; }
        public Beat[] beats { get; set; }
        public Section[] sections { get; set; }
        public Segment[] segments { get; set; }
        public Tatum[] tatums { get; set; }
        public Track track { get; set; }
    }

    public class Track
    {
        public float duration { get; set; }
        public string sample_md5 { get; set; }
        public float offset_seconds { get; set; }
        public float window_seconds { get; set; }
        public float analysis_sample_rate { get; set; }
        public float analysis_channels { get; set; }
        public float end_of_fade_in { get; set; }
        public float start_of_fade_out { get; set; }
        public float loudness { get; set; }
        public float tempo { get; set; }
        public float tempo_confidence { get; set; }
        public float time_signature { get; set; }
        public float time_signature_confidence { get; set; }
        public float key { get; set; }
        public float key_confidence { get; set; }
        public float mode { get; set; }
        public float mode_confidence { get; set; }
        public string codestring { get; set; }
        public float code_version { get; set; }
        public string echoprintstring { get; set; }
        public float echoprint_version { get; set; }
        public string synchstring { get; set; }
        public float synch_version { get; set; }
        public string rhythmstring { get; set; }
        public float rhythm_version { get; set; }
    }

    public class Bar
    {
        public float start { get; set; }
        public float duration { get; set; }
        public float confidence { get; set; }
    }

    public class Beat
    {
        public float start { get; set; }
        public float duration { get; set; }
        public float confidence { get; set; }
    }

    public class Section
    {
        public float start { get; set; }
        public float duration { get; set; }
        public float confidence { get; set; }
        public float loudness { get; set; }
        public float tempo { get; set; }
        public float tempo_confidence { get; set; }
        public float key { get; set; }
        public float key_confidence { get; set; }
        public float mode { get; set; }
        public float mode_confidence { get; set; }
        public float time_signature { get; set; }
        public float time_signature_confidence { get; set; }
    }

    public class Segment
    {
        public float start { get; set; }
        public float duration { get; set; }
        public float confidence { get; set; }
        public float loudness_start { get; set; }
        public float loudness_max_time { get; set; }
        public float loudness_max { get; set; }
        public float loudness_end { get; set; }
        public float[] pitches { get; set; }
        public float[] timbre { get; set; }
    }

    public class Tatum
    {
        public float start { get; set; }
        public float duration { get; set; }
        public float confidence { get; set; }
    }

}
