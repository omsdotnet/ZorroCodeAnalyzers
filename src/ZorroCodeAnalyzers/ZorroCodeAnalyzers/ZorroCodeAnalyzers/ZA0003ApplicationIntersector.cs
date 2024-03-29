﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace ZorroCodeAnalyzers
{
  [DiagnosticAnalyzer(LanguageNames.CSharp)]
  public class ZA0003ApplicationIntersector : DiagnosticAnalyzer
  {
    public const string DiagnosticId = "ZA0003";
    private const string Category = "Architecture";
    private const string KeyWord = "Application";

    private static readonly string[] rejectedKeyWord = { "Infrastructure" };

    private static readonly LocalizableString title = new LocalizableResourceString(nameof(Resources.ZA0003Title), Resources.ResourceManager, typeof(Resources));
    private static readonly LocalizableString messageFormat = new LocalizableResourceString(nameof(Resources.ZA0003MessageFormat), Resources.ResourceManager, typeof(Resources));
    private static readonly LocalizableString description = new LocalizableResourceString(nameof(Resources.ZA0003Description), Resources.ResourceManager, typeof(Resources));

    private static readonly DiagnosticDescriptor rule = new DiagnosticDescriptor(DiagnosticId, title, messageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(rule);

    public override void Initialize(AnalysisContext context)
    {
      context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
      context.EnableConcurrentExecution();

      context.RegisterSyntaxTreeAction(AnalyzeContext);
    }

    private static void AnalyzeContext(SyntaxTreeAnalysisContext context)
    {
      var root = context.Tree.GetCompilationUnitRoot();

      var namespaceName = root.Members
        .Where(x => x is NamespaceDeclarationSyntax)
        .Select(x => ((NamespaceDeclarationSyntax)x).Name.ToString())
        .FirstOrDefault();

      if (string.IsNullOrEmpty(namespaceName))
      {
        return;
      }

      var keyWordPosition = GetKeyWordPosition(namespaceName, KeyWord);

      if (keyWordPosition == -1)
      {
        return;
      }

      var usingNodes = root.Usings
        .Select(x => GetPositionValue(x.Name.ToString(), keyWordPosition))
        .ToArray();

      var count = 0;
      foreach (var usingItem in usingNodes)
      {
        if (usingItem != null && rejectedKeyWord.Contains(usingItem))
        {
          var location = root.Usings[count].Name.GetLocation();
          var diagnostic = Diagnostic.Create(rule, location, usingItem);
          context.ReportDiagnostic(diagnostic);
        }

        count++;
      }
    }

    private static int GetKeyWordPosition(string route, string keyword)
    {
      if (string.IsNullOrEmpty(route))
      {
        return -1;
      }

      var segments = route.Split('.');

      return Array.IndexOf(segments, keyword);
    }

    private static string GetPositionValue(string route, int keyWordPosition)
    {
      if (string.IsNullOrEmpty(route))
      {
        return null;
      }

      var segments = route.Split('.');

      if (segments.Length <= keyWordPosition)
      {
        return null;
      }

      return segments[keyWordPosition];
    }
  }
}
