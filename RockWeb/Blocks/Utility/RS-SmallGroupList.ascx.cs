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

using Rock;
using Rock.Attribute;
using Rock.Data;
using Rock.Model;
using Rock.Web.Cache;
using Rock.Web.UI;
using Rock.Web.UI.Controls;

namespace RockWeb.Blocks.Utility
{
    [DisplayName( "RS Small Group List" )]
    [Category( "Utility" )]
    [Description( "Lists all active small groups and allows navigation to a detail page." )]

    #region Block Attributes

    [LinkedPage(
        "Detail Page",
        Description = "The page to navigate to when a small group is selected.",
        Key = AttributeKey.DetailPage,
        Order = 0 )]

    #endregion Block Attributes

    [Rock.SystemGuid.BlockTypeGuid( "6A17F98A-8B76-4C3B-A7A0-2B3C4F5D6E7F" )]
    public partial class RSSmallGroupList : RockBlock
    {
        #region Attribute Keys

        private static class AttributeKey
        {
            public const string DetailPage = "DetailPage";
        }

        #endregion

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
            gSmallGroups.GridRebind += gSmallGroups_GridRebind;

            // This event gets fired after block settings are updated. It's nice to repaint the screen if these settings would alter it.
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
                BindGrid();
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
            BindGrid();
        }

        /// <summary>
        /// Handles the GridRebind event of the gSmallGroups control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void gSmallGroups_GridRebind( object sender, EventArgs e )
        {
            BindGrid();
        }

        /// <summary>
        /// Handles the RowSelected event of the gSmallGroups control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowEventArgs"/> instance containing the event data.</param>
        protected void gSmallGroups_RowSelected( object sender, RowEventArgs e )
        {
            var groupId = e.RowKeyId;
            var queryParams = new System.Collections.Generic.Dictionary<string, string>();
            queryParams.Add( PageParameterKey.GroupId, groupId.ToString() );

            var detailPageUrl = LinkedPageUrl( AttributeKey.DetailPage, queryParams );
            if ( !string.IsNullOrEmpty( detailPageUrl ) )
            {
                NavigateToLinkedPage( AttributeKey.DetailPage, queryParams );
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Binds the grid with active small groups.
        /// </summary>
        private void BindGrid()
        {
            var rockContext = new RockContext();
            var groupService = new GroupService( rockContext );

            // Get the "Small Group" group type
            var groupTypeService = new GroupTypeService( rockContext );
            var smallGroupType = groupTypeService.Queryable()
                .AsNoTracking()
                .FirstOrDefault( gt => gt.Name == "Small Group" );

            if ( smallGroupType == null )
            {
                return;
            }

            // Query for active small groups
            var query = groupService.Queryable()
                .AsNoTracking()
                .Where( g => g.IsActive && g.GroupTypeId == smallGroupType.Id )
                .Select( g => new
                {
                    g.Id,
                    g.Name,
                    g.Description,
                    g.GroupCapacity,
                    g.CreatedDateTime,
                    g.ModifiedDateTime
                } );

            // Apply sorting
            var sortProperty = gSmallGroups.SortProperty;
            if ( gSmallGroups.AllowSorting && sortProperty != null )
            {
                query = query.Sort( sortProperty );
            }
            else
            {
                query = query.OrderBy( g => g.Name );
            }

            // Bind the grid
            gSmallGroups.SetLinqDataSource( query );
            gSmallGroups.DataBind();
        }

        #endregion
    }
}
