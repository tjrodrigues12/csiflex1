using System;
using System.Collections.Generic;
using System.Text;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class GeniusCollectionEntity<T>
    {
        public Message[] Messages { get; set; }
        public T[] Result { get; set; }
    }
    public class Message
    {
        public string English { get; set; }
        public string French { get; set; }
        public object Index { get; set; }
        public object InnerException { get; set; }
        public string Key { get; set; }
        public int MessageType { get; set; }
        public int ProcessingType { get; set; }
        public string Source { get; set; }
        public int Tag { get; set; }
    }
}
