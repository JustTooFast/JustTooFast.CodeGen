// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public partial class ValueEmitter
{
    private partial void Validate()
    {
        int count =
            (m_Value.ObjectValue  is null ? 0 : 1) +
            (m_Value.ArrayValue   is null ? 0 : 1) +
            (m_Value.StringValue  is null ? 0 : 1) +
            (m_Value.NumberValue  is null ? 0 : 1) +
            (m_Value.BooleanValue is null ? 0 : 1) +
            (m_Value.NullValue    is null ? 0 : 1);

        if (count != 1)
            throw new JsonFormatException("Value must have exactly one of: ObjectValue, ArrayValue, StringValue, NumberValue, BooleanValue, NullValue.");
    }

    public partial void EmitTo(IAppender appender)
    {
        if (m_Value.ObjectValue is not null)  { new ObjectValueEmitter(m_Value.ObjectValue).EmitTo(appender); return; }
        if (m_Value.ArrayValue is not null)   { new ArrayValueEmitter(m_Value.ArrayValue).EmitTo(appender); return; }
        if (m_Value.StringValue is not null)  { new StringValueEmitter(m_Value.StringValue).EmitTo(appender); return; }
        if (m_Value.NumberValue is not null)  { new NumberValueEmitter(m_Value.NumberValue).EmitTo(appender); return; }
        if (m_Value.BooleanValue is not null) { new BooleanValueEmitter(m_Value.BooleanValue).EmitTo(appender); return; }
        /* NullValue */
        new NullValueEmitter(m_Value.NullValue!).EmitTo(appender);
    }
}
