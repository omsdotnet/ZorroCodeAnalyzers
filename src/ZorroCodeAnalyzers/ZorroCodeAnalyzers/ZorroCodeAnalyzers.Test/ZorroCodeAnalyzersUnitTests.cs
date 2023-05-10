using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Testing;
using VerifyCS = ZorroCodeAnalyzers.Test.CSharpCodeFixVerifier<
    ZorroCodeAnalyzers.ZA0001SlicesIntersector,
    ZorroCodeAnalyzers.UsingRemoverCodeFixProvider>;
using System.Collections.Generic;

namespace ZorroCodeAnalyzers.Test
{
  [TestClass]
  public class ZorroCodeAnalyzersUnitTest
  {
    //No diagnostics expected to show up
    [TestMethod]
    public async Task EmptyTest()
    {
      var test = @"";

      await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task HappyTest()
    {
      var test = @"
        using FeaturesAnalyzer.Debug.Features.FeatureOne;

        namespace FeaturesAnalyzer.Debug.Features.FeatureTwo
        {
          class Buzz
          {
            public Fizz Value { get; set; }
          }
        }
      ";

      var expected1 = DiagnosticResult.CompilerError("ZA0001").WithSpan(2, 15, 2, 57).WithArguments("FeatureTwo", "FeatureOne");
      var expected2 = DiagnosticResult.CompilerError("CS0234").WithSpan(2, 47, 2, 57).WithArguments("FeatureOne", "FeaturesAnalyzer.Debug.Features");
      var expected3 = DiagnosticResult.CompilerError("CS0246").WithSpan(8, 20, 8, 24).WithArguments("Fizz");

      await VerifyCS.VerifyAnalyzerAsync(test, expected1, expected2, expected3);
    }

    [TestMethod]
    public async Task HappySettingsTest()
    {
      var test = @"
        using FeaturesAnalyzer.Debug.Proxy.FeatureOne;

        namespace FeaturesAnalyzer.Debug.Proxy.FeatureTwo
        {
          class Buzz
          {
            public Fizz Value { get; set; }
          }
        }
      ";

      var settings = @"
        {
          ""ZA0001"": ""Proxy""
        }
      ";

      var settingsFile = new List<(string Name, string Content)>()
      {
        (Name: "ZorroCodeAnalyzers.config", Content: settings)
      };

      var expected1 = DiagnosticResult.CompilerError("ZA0001").WithSpan(2, 15, 2, 54).WithArguments("FeatureTwo", "FeatureOne");
      var expected2 = DiagnosticResult.CompilerError("CS0234").WithSpan(2, 44, 2, 54).WithArguments("FeatureOne", "FeaturesAnalyzer.Debug.Proxy");
      var expected3 = DiagnosticResult.CompilerError("CS0246").WithSpan(8, 20, 8, 24).WithArguments("Fizz");

      await VerifyCS.VerifyAnalyzerAsync(test, settingsFile, expected1, expected2, expected3);
    }


    //Diagnostic and CodeFix both triggered and checked for
    //[TestMethod]
    public async Task CodeFixTest()
    {
      var test = @"
        using FeaturesAnalyzer.Debug.Features.FeatureOne;

        namespace FeaturesAnalyzer.Debug.Features.FeatureTwo
        {
          class Buzz
          {
            public Fizz Value { get; set; }
          }
        }
      ";

      var fixtest = @"

        namespace FeaturesAnalyzer.Debug.Features.FeatureTwo
        {
          class Buzz
          {
            public Fizz Value { get; set; }
          }
        }
      ";

      var expected1 = DiagnosticResult.CompilerError("ZA0001").WithSpan(2, 15, 2, 57).WithArguments("FeatureTwo", "FeatureOne");
      var expected2 = DiagnosticResult.CompilerError("CS0234").WithSpan(2, 47, 2, 57).WithArguments("FeatureOne", "FeaturesAnalyzer.Debug.Features");
      var expected3 = DiagnosticResult.CompilerError("CS0246").WithSpan(8, 20, 8, 24).WithArguments("Fizz");

      await VerifyCS.VerifyCodeFixAsync(test, new []{ expected1, expected2, expected3 }, fixtest);
    }
  }
}
