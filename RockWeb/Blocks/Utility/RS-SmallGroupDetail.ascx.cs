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
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using Rock;
using Rock.Attribute;
using Rock.Data;
using Rock.Model;
using Rock.Web.UI;

namespace RockWeb.Blocks.Utility
{
    [DisplayName( "RS Small Group Detail" )]
    [Category( "Utility" )]
    [Description( "Displays detailed information about a selected small group." )]

    [Rock.SystemGuid.BlockTypeGuid( "7F2E3A9B-8C4D-5E6F-7A0B-9C1D2E3F4A5B" )]
    public partial class RSSmallGroupDetail : RockBlock
    {
        #region PageParameterKeys

        private static class PageParameterKey
        {
            public const string GroupId = "GroupId";
        }

        #endregion

        #region Base Control Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit( EventArgs e )
        {
            base.OnInit( e );

            // This event gets fired after block settings are updated.
            this.BlockUpdated += Block_BlockUpdated;
            this.AddConfigurationUpdateTrigger( upnlContent );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad( EventArgs e )
        {
            if ( !Page.IsPostBack )
            {
                LoadGroupDetail();
            }

            base.OnLoad( e );
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the BlockUpdated event of the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Block_BlockUpdated( object sender, EventArgs e )
        {
            LoadGroupDetail();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the group detail information.
        /// </summary>
        private void LoadGroupDetail()
        {
            var groupId = PageParameter( PageParameterKey.GroupId ).AsInteger();

            if ( groupId <= 0 )
            {
                ShowErrorMessage( "Group not found." );
                return;
            }

            var rockContext = new RockContext();
            var groupService = new GroupService( rockContext );
            var group = groupService.Queryable()
                .AsNoTracking()
                .FirstOrDefault( g => g.Id == groupId );

            if ( group == null )
            {
                ShowErrorMessage( "Group not found." );
                return;
            }

            // Populate the controls with group information
            lTitle.Text = group.Name;
            lName.Text = group.Name;
            lDescription.Text = group.Description ?? "(No description)";
            lDateCreated.Text = group.CreatedDateTime.HasValue
                ? group.CreatedDateTime.Value.ToString( "g" )
                : "(Unknown)";
            lDateModified.Text = group.ModifiedDateTime.HasValue
                ? group.ModifiedDateTime.Value.ToString( "g" )
                : "(Unknown)";
            lGroupCapacity.Text = group.GroupCapacity.HasValue
                ? group.GroupCapacity.Value.ToString()
                : "(No limit)";

            // Set the entity for audit details
            pdAuditDetails.SetEntity( group, ResolveRockUrl( "~" ) );
        }

        /// <summary>
        /// Shows an error message to the user.
        /// </summary>
        /// <param name="message">The error message.</param>
        private void ShowErrorMessage( string message )
        {
            pnlView.Visible = false;
            lTitle.Text = "Error";
            lName.Text = message;
        }

        #endregion
    }
}
