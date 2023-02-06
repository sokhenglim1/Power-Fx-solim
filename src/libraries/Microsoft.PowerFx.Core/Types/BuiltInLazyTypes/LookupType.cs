// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.PowerFx.Core.Utils;
using Microsoft.PowerFx.Types;

namespace Microsoft.PowerFx.Core.Types.BuiltInLazyTypes
{
    // Type represents attachment type which are delay loaded fields at runtime
    internal class LookupType : RecordType
    {
        public DType Lookup { get; }

        public LookupType(DType lookupType) 
            : base()
        {
            Contracts.Assert(lookupType.IsRecord);

            Lookup = lookupType;
        }
        
        public override IEnumerable<string> FieldNames => Lookup.GetRootFieldNames().Select(name => name.Value);

        // public override string UserVisibleTypeName => "Attachment";

        public override bool TryGetFieldType(string name, out FormulaType type)
        {
            if (!Lookup.TryGetType(new DName(name), out var dType))
            {
                type = Blank;
                return false;
            }

            type = Build(dType);
            return true;
        }

        public override bool Equals(object other)
        {
            if (other is not LookupType otherLookupType)
            {
                return false;
            }

            return Lookup.Equals(otherLookupType.Lookup);
        }

        public override int GetHashCode()
        {
            return Lookup.GetHashCode();
        }
    }
}
