﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Splunk.Examples.SharePointWebPart.IndexSummaryWebPart {
    using System.Web;
    using System.Text.RegularExpressions;
    using Microsoft.SharePoint.WebPartPages;
    using Microsoft.SharePoint.WebControls;
    using System.Web.Security;
    using Microsoft.SharePoint.Utilities;
    using System.Web.UI;
    using System;
    using System.Web.UI.WebControls;
    using System.Collections.Specialized;
    using Microsoft.SharePoint;
    using System.Collections;
    using System.Web.Profile;
    using System.Text;
    using System.Web.Caching;
    using System.Configuration;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.SessionState;
    using System.Web.UI.HtmlControls;
    
    
    public partial class IndexSummaryWebPart {
        
        protected global::System.Web.UI.WebControls.GridView IndexSummaryGridView;
        
        public static implicit operator global::System.Web.UI.TemplateControl(IndexSummaryWebPart target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control2(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            @__ctrl.BackColor = System.Drawing.Color.Gainsboro;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control3(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            @__ctrl.BackColor = ((System.Drawing.Color)(System.Drawing.Color.FromArgb(204, 204, 204)));
            @__ctrl.ForeColor = System.Drawing.Color.Black;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control4(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            @__ctrl.BackColor = ((System.Drawing.Color)(System.Drawing.Color.FromArgb(0, 0, 132)));
            @__ctrl.Font.Bold = true;
            @__ctrl.ForeColor = System.Drawing.Color.White;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control5(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            @__ctrl.BackColor = ((System.Drawing.Color)(System.Drawing.Color.FromArgb(153, 153, 153)));
            @__ctrl.ForeColor = System.Drawing.Color.Black;
            @__ctrl.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control6(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            @__ctrl.BackColor = ((System.Drawing.Color)(System.Drawing.Color.FromArgb(238, 238, 238)));
            @__ctrl.ForeColor = System.Drawing.Color.Black;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControl__control7(System.Web.UI.WebControls.TableItemStyle @__ctrl) {
            @__ctrl.BackColor = ((System.Drawing.Color)(System.Drawing.Color.FromArgb(0, 138, 140)));
            @__ctrl.Font.Bold = true;
            @__ctrl.ForeColor = System.Drawing.Color.White;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.GridView @__BuildControlIndexSummaryGridView() {
            global::System.Web.UI.WebControls.GridView @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.GridView();
            this.IndexSummaryGridView = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "IndexSummaryGridView";
            @__ctrl.BackColor = System.Drawing.Color.White;
            @__ctrl.BorderColor = ((System.Drawing.Color)(System.Drawing.Color.FromArgb(153, 153, 153)));
            @__ctrl.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            @__ctrl.BorderWidth = new System.Web.UI.WebControls.Unit(1, System.Web.UI.WebControls.UnitType.Pixel);
            @__ctrl.CellPadding = 3;
            @__ctrl.EnableModelValidation = true;
            @__ctrl.GridLines = System.Web.UI.WebControls.GridLines.Vertical;
            this.@__BuildControl__control2(@__ctrl.AlternatingRowStyle);
            this.@__BuildControl__control3(@__ctrl.FooterStyle);
            this.@__BuildControl__control4(@__ctrl.HeaderStyle);
            this.@__BuildControl__control5(@__ctrl.PagerStyle);
            this.@__BuildControl__control6(@__ctrl.RowStyle);
            this.@__BuildControl__control7(@__ctrl.SelectedRowStyle);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::Splunk.Examples.SharePointWebPart.IndexSummaryWebPart.IndexSummaryWebPart @__ctrl) {
            global::System.Web.UI.WebControls.GridView @__ctrl1;
            @__ctrl1 = this.@__BuildControlIndexSummaryGridView();
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n"));
        }
        
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}
