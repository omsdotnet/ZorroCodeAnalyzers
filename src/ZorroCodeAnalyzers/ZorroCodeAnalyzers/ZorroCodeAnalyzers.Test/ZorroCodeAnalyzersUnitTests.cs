using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Testing;
using VerifyCS = ZorroCodeAnalyzers.Test.CSharpCodeFixVerifier<
    ZorroCodeAnalyzers.ZA0001FeaturesIntersector,
    ZorroCodeAnalyzers.ZA0001FeaturesIntersectorCodeFixProvider>;

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


    //Diagnostic and CodeFix both triggered and checked for
    //[TestMethod]
    public async Task TestMethod2()
    {
      var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class {|#0:TypeName|}
        {   
        }
    }";

      var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class TYPENAME
        {   
        }
    }";

      var expected = VerifyCS.Diagnostic("ZorroCodeAnalyzers").WithLocation(0).WithArguments("TypeName");
      await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
    }
  }
}
