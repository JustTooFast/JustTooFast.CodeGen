// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

public interface IOutputSink : IDisposable
{
    IAppender CreateAppender();
    void Complete();
}
