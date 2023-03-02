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
    // Type represents lookup type which are delay loaded fields at runtime
    internal class LookupType : RecordType
    {
        public override IEnumerable<string> FieldNames { get; }

        public override string UserVisibleTypeName => "LookupType";

        public delegate bool TryGetFieldDelegate(string name, out FormulaType type);

        private readonly TryGetFieldDelegate _tryGetField;

        internal string Identity;

        public LookupType(string id, IEnumerable<string> fieldNames, TryGetFieldDelegate getter)
            : base()
        {
            FieldNames = fieldNames;
            _tryGetField = getter;
            Identity = id;
        }

        public override bool TryGetFieldType(string name, out FormulaType type)
        {
            /* if (!_tryGetField(name, out var dType))
             {
                 type = Blank;
                 return false;
             }

             type = Build(dType);
             return true;*/
            return _tryGetField(name, out type);
        }

        public override bool Equals(object other)
        {
            return other is LookupType otherTable &&
                    Identity == otherTable.Identity;
        }

        public override int GetHashCode()
        {
            return Identity.GetHashCode();
        }
    }
}
