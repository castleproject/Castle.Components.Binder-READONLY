// Copyright 2004-2007 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.MonoRail.Framework.Helpers
{
    public interface IJSElementGenerator {
        /// <summary>
        /// Gets the parent generator.
        /// </summary>
        /// <value>The parent generator.</value>
        IJSGenerator ParentGenerator { get; }

        /// <summary>
        /// TODO: Document this
        /// </summary>
        /// <param name="renderOptions">The render options.</param>
        void ReplaceHtml(object renderOptions);

        /// <summary>
        /// TODO: Document this
        /// </summary>
        /// <param name="renderOptions">The render options.</param>
        void Replace(object renderOptions);
    }
}
