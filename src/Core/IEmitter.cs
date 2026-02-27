// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen;

public interface IEmitter
{
    void EmitTo(IAppender appender);
}
