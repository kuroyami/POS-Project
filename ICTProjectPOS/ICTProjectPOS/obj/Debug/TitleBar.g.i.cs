﻿#pragma checksum "..\..\TitleBar.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2555490633AB0A751D8D9793060CE15C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ICTProjectPOS {
    
    
    /// <summary>
    /// TitleBar
    /// </summary>
    public partial class TitleBar : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 81 "..\..\TitleBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlock_Username;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\TitleBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_SignOut;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\TitleBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Tab_Summary;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\TitleBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Tab_Tables;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\TitleBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Tab_Reservations;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\TitleBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Tab_Reports;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ICTProjectPOS;component/titlebar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TitleBar.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TextBlock_Username = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.Button_SignOut = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.Tab_Summary = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\TitleBar.xaml"
            this.Tab_Summary.Click += new System.Windows.RoutedEventHandler(this.Tab_Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Tab_Tables = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\TitleBar.xaml"
            this.Tab_Tables.Click += new System.Windows.RoutedEventHandler(this.Tab_Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Tab_Reservations = ((System.Windows.Controls.Button)(target));
            
            #line 114 "..\..\TitleBar.xaml"
            this.Tab_Reservations.Click += new System.Windows.RoutedEventHandler(this.Tab_Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Tab_Reports = ((System.Windows.Controls.Button)(target));
            
            #line 119 "..\..\TitleBar.xaml"
            this.Tab_Reports.Click += new System.Windows.RoutedEventHandler(this.Tab_Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

