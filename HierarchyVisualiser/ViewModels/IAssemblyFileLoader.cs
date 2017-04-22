﻿namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Loads the assembly from file.
    /// </summary>
    internal interface IAssemblyFileLoader
    {
        bool TryLoadAssemblyFromFile(string file);
        void LoadAssemblyFromFile(string file);
    }
}
