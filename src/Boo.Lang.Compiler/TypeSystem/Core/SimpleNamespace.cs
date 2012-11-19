﻿#region license
// Copyright (c) 2003, 2004, 2005 Rodrigo B. de Oliveira (rbo@acm.org)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of Rodrigo B. de Oliveira nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System.Collections.Generic;
using Boo.Lang.Environments;
using Boo.Lang.Compiler.TypeSystem.Services;

namespace Boo.Lang.Compiler.TypeSystem.Core
{
	public class SimpleNamespace : AbstractNamespace
	{		
		protected INamespace _parent;
		protected IEnumerable<IEntity> _members;
		protected IDictionary<string,string> _aliases;
		
		public SimpleNamespace(INamespace parent, IEnumerable<IEntity> members)
		{
			_parent = parent;
			_members = members;			
		}

		public SimpleNamespace(INamespace parent, IEnumerable<IEntity> members, IDictionary<string,string> aliases) : this(parent, members)
		{
			_aliases = aliases;
		}
		
		public override INamespace ParentNamespace
		{
			get { return _parent; }
		}
		
		public override IEnumerable<IEntity> GetMembers()
		{
			return _members;
		}

        public override bool Resolve(ICollection<IEntity> resultingSet, string name, EntityType typesToConsider)
        {   
        	if (null != _aliases && _aliases.Count > 0) {
        		if (!_aliases.ContainsKey(name)) {
        			return false;
        		}

        		name = _aliases[name];
        	}

        	return base.Resolve(resultingSet, name, typesToConsider);
            //return My<NameResolutionService>.Instance.Resolve(name, GetMembers(), typesToConsider, resultingSet);
        }
	}
}