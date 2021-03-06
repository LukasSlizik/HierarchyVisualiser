﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System;
using HierarchyVisualiser.Commands;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents an .NET Assembly as a collection of Namespaces, identified by it's name.
    /// </summary>
    internal class AssemblyViewModel : ViewModelBase
    {
        private Assembly _wrapeedAssembly;
        private ObservableCollection<NamespaceViewModel> _namespaces;
        public event EventHandler AssemblyRemoved;
        public event EventHandler SelectionChanged;

        public AssemblyViewModel(Assembly assembly)
        {
            WrappedAssembly = assembly;
            PopulateNamespaces();

            RegisterCommands();
        }

        /// <summary>
        /// Command for removing the assembly from the navigation tree.
        /// </summary>
        public RelayCommand RemoveCommand { get; set; }

        /// <summary>
        /// Register all Commands.
        /// </summary>
        private void RegisterCommands()
        {
            RemoveCommand = new RelayCommand(OnRemoveAssemblyExecute);
        }

        /// <summary>
        /// Can assembly be removed from the navigation ?
        /// </summary>
        private void OnRemoveAssemblyExecute()
        {
            AssemblyRemoved?.Invoke(this, null);
        }

        /// <summary>
        /// Assembly is selected or deselected.
        /// </summary>
        private void OnSelectionChanged(object sender, EventArgs args)
        {
            SelectionChanged?.Invoke(sender, null);
        }

        /// <summary>
        /// Populates the collection of all namespaces contained in this assembly.
        /// </summary>
        private void PopulateNamespaces()
        {
            var namespaces = WrappedAssembly.GetTypes().Select(t => t.Namespace).Distinct();
            Namespaces = new ObservableCollection<NamespaceViewModel>(namespaces.Select(ns => new NamespaceViewModel(ns, WrappedAssembly)));
            foreach (var ns in Namespaces)
                ns.SelectionChanged += OnSelectionChanged;
        }

        /// <summary>
        /// FullName of the assembly.
        /// </summary>
        public string FullName => WrappedAssembly.FullName;

        /// <summary>
        /// Underlying assembly.
        /// </summary>
        public Assembly WrappedAssembly
        {
            get
            {
                return _wrapeedAssembly;
            }
            set
            {
                if (_wrapeedAssembly == value)
                    return;
                _wrapeedAssembly = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Collection of all Namespaces contained within the Assembly.
        /// </summary>
        public ObservableCollection<NamespaceViewModel> Namespaces
        {
            get
            {
                return _namespaces;
            }
            set
            {
                if (_namespaces == value)
                    return;

                _namespaces = value;
                RaisePropertyChanged();
            }
        }

    }
}
