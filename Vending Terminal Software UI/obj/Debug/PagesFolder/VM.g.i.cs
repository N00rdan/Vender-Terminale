﻿#pragma checksum "..\..\..\PagesFolder\VM.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FDD9459D64CB050606214FAC935C70A715530EEF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using Vending_Terminal_Software_UI.PagesFolder;


namespace Vending_Terminal_Software_UI.PagesFolder {
    
    
    /// <summary>
    /// VM
    /// </summary>
    public partial class VM : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\PagesFolder\VM.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Current;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\PagesFolder\VM.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddMoney;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\PagesFolder\VM.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ProductsAdd;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\PagesFolder\VM.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxSupplies;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\PagesFolder\VM.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Buy;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\PagesFolder\VM.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Change;
        
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
            System.Uri resourceLocater = new System.Uri("/Vending Terminal Software UI;component/pagesfolder/vm.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PagesFolder\VM.xaml"
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
            
            #line 9 "..\..\..\PagesFolder\VM.xaml"
            ((Vending_Terminal_Software_UI.PagesFolder.VM)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Current = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.AddMoney = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\PagesFolder\VM.xaml"
            this.AddMoney.Click += new System.Windows.RoutedEventHandler(this.AddMoney_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ProductsAdd = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\PagesFolder\VM.xaml"
            this.ProductsAdd.Click += new System.Windows.RoutedEventHandler(this.ProductsAdd_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.listBoxSupplies = ((System.Windows.Controls.ListBox)(target));
            
            #line 25 "..\..\..\PagesFolder\VM.xaml"
            this.listBoxSupplies.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listBoxSupplies_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Buy = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\PagesFolder\VM.xaml"
            this.Buy.Click += new System.Windows.RoutedEventHandler(this.Buy_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Change = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\PagesFolder\VM.xaml"
            this.Change.Click += new System.Windows.RoutedEventHandler(this.Change_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

