﻿#pragma checksum "..\..\Reservation_Blueprint_CreateNewPopup.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "52CE0ACDF73C872D32AD195D130436B3"
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
    /// Reservation_Blueprint_CreateNewPopup
    /// </summary>
    public partial class Reservation_Blueprint_CreateNewPopup : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\Reservation_Blueprint_CreateNewPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run TextBlock_Table;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Reservation_Blueprint_CreateNewPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run TextBlock_Date;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Reservation_Blueprint_CreateNewPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run TextBlock_Time;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\Reservation_Blueprint_CreateNewPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_CreateReservation;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Reservation_Blueprint_CreateNewPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Cancel;
        
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
            System.Uri resourceLocater = new System.Uri("/ICTProjectPOS;component/reservation_blueprint_createnewpopup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Reservation_Blueprint_CreateNewPopup.xaml"
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
            this.TextBlock_Table = ((System.Windows.Documents.Run)(target));
            return;
            case 2:
            this.TextBlock_Date = ((System.Windows.Documents.Run)(target));
            return;
            case 3:
            this.TextBlock_Time = ((System.Windows.Documents.Run)(target));
            return;
            case 4:
            this.Button_CreateReservation = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\Reservation_Blueprint_CreateNewPopup.xaml"
            this.Button_CreateReservation.Click += new System.Windows.RoutedEventHandler(this.Button_CreateReservation_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Button_Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\Reservation_Blueprint_CreateNewPopup.xaml"
            this.Button_Cancel.Click += new System.Windows.RoutedEventHandler(this.Button_Cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

