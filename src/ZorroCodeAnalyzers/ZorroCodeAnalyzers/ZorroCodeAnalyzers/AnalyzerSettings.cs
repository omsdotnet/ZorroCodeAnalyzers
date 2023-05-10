using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ZorroCodeAnalyzers
{
  [DataContract]
  public class AnalyzerSettings
  {
    [DataMember]
    public string ZA0001 { get; set; }
  }
}
