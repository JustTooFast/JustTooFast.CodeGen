// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen;

public static class AppenderExtensions
{
    public static IIndentingAppender EnsureIndenting(this IAppender appender)
        => appender as IIndentingAppender ?? new IndentScopeAppender(appender);

    public static void BlankLine(this IAppender a) => a.AppendLine();
}
