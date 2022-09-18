using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ZorroCodeAnalyzers
{
  [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UsingRemoverCodeFixProvider))]
  [Shared]
  public class UsingRemoverCodeFixProvider : CodeFixProvider
  {
    public sealed override ImmutableArray<string> FixableDiagnosticIds =>
      ImmutableArray.Create(ZA0001FeaturesIntersector.DiagnosticId, 
        ZA0002DomainIntersector.DiagnosticId,
        ZA0003ApplicationIntersector.DiagnosticId, 
        ZA0004InfrastructureIntersector.DiagnosticId);

    public sealed override FixAllProvider GetFixAllProvider()
    {
      return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
      var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

      var diagnostic = context.Diagnostics.First();
      var diagnosticSpan = diagnostic.Location.SourceSpan;

      var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<UsingDirectiveSyntax>()
        .First();

      context.RegisterCodeFix(
        CodeAction.Create(
          CodeFixResources.CodeFixTitle,
          c => RemoveNodeAsync(context.Document, declaration, c),
          nameof(CodeFixResources.CodeFixTitle)),
        diagnostic);
    }

    private async Task<Document> RemoveNodeAsync(Document document, UsingDirectiveSyntax usingDeclaration,
      CancellationToken cancellationToken)
    {
      var oldRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);

      return document.WithSyntaxRoot(oldRoot.RemoveNode(usingDeclaration, SyntaxRemoveOptions.KeepNoTrivia));
    }
  }
}