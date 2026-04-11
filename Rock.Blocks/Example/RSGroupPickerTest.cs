// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System.ComponentModel;

using Rock.Attribute;
using Rock.Model;
using Rock.ViewModels.Blocks.Example.RSGroupPickerTest;

namespace Rock.Blocks.Example
{
    /// <summary>
    /// Test block for the RS Group Picker control.
    /// </summary>
    [DisplayName( "RS Group Picker Test" )]
    [Category( "Obsidian > Example" )]
    [Description( "Test block demonstrating the RS group picker control." )]
    [IconCssClass( "ti ti-users-group" )]
    [SupportedSiteTypes( Model.SiteType.Web )]

    [Rock.SystemGuid.EntityTypeGuid( "A5C84D3F-8B9E-4D5C-B1F8-7A2E9C6D3F4A" )]
    [Rock.SystemGuid.BlockTypeGuid( "B6D95E4F-9C0F-5E6D-C2F9-8B3F0D7E4F5B" )]
    public class RSGroupPickerTest : RockBlockType
    {
        /// <inheritdoc/>
        public override object GetObsidianBlockInitialization()
        {
            return new RSGroupPickerTestInitializationBox();
        }
    }
}
