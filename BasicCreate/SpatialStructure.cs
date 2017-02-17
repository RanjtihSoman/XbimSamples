﻿using System;
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace BasicExamples
{
    class SpatialStructureExample
    {
        public static void Show()
        {
            const string file = "SampleHouse.ifc";
            
            using (var model = IfcStore.Open(file))
            {
                var project = model.Instances.FirstOrDefault<IIfcProject>();
                PrintHierarchy(project, 0);
            }
        }

        private static void PrintHierarchy(IIfcObjectDefinition o, int level)
        {
            Console.WriteLine($"{GetIndent(level)}{o.Name} [{o.GetType().Name}]");
            foreach (var item in o.IsDecomposedBy.SelectMany(r => r.RelatedObjects))
                PrintHierarchy(item, level +1);
        }

        private static string GetIndent(int level)
        {
            var indent = "";
            for (int i = 0; i < level; i++)
                indent += "  ";
            return indent;
        }
    }
}