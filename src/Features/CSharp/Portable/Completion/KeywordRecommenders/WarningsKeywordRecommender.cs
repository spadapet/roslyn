﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Extensions.ContextQuery;

namespace Microsoft.CodeAnalysis.CSharp.Completion.KeywordRecommenders;

internal sealed class WarningsKeywordRecommender() : AbstractSyntacticSingleKeywordRecommender(SyntaxKind.WarningsKeyword, isValidInPreprocessorContext: true)
{
    protected override bool IsValidContext(int position, CSharpSyntaxContext context, CancellationToken cancellationToken)
    {
        var previousToken1 = context.TargetToken;
        var previousToken2 = previousToken1.GetPreviousToken(includeSkipped: true);
        var previousToken3 = previousToken2.GetPreviousToken(includeSkipped: true);

        // # nullable enable |
        // # nullable enable w|
        return
            (previousToken1.Kind() == SyntaxKind.EnableKeyword || previousToken1.Kind() == SyntaxKind.DisableKeyword || previousToken1.Kind() == SyntaxKind.RestoreKeyword) &&
            previousToken2.Kind() == SyntaxKind.NullableKeyword &&
            previousToken3.Kind() == SyntaxKind.HashToken;
    }
}
