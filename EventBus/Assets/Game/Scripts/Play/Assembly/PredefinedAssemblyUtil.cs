using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class PredefinedAssemblyUtil
    {
        enum AssemblyTpe
        {
            AssemblyCSharp,
            AssemblyCSharpEditor,
            AssemblyCSharpEditorFirstPass,
            AssemblyCSharpFirstPass,
        }

        static AssemblyTpe? GetAssemblyType(string name)
        {

            return name switch
            {
                "Assembly-CSharp" => AssemblyTpe.AssemblyCSharp,
                "Assembly-CSharp-Editor" => AssemblyTpe.AssemblyCSharpEditor,
                "Assembly-CSharp-Editor-firstpass" => AssemblyTpe.AssemblyCSharpEditorFirstPass,
                "Assembly-CSharp-firstpass" => AssemblyTpe.AssemblyCSharpFirstPass,
                _ => null
            };

        }

        public static List<Type> GetTypes(Type interfaceType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Dictionary<AssemblyTpe, Type[]> assemblyTypes = new Dictionary<AssemblyTpe, Type[]>();
            List<Type> types = new List<Type>();
            for (var i = 0; i < assemblies.Length; i++)
            {
                AssemblyTpe? assemblyTpe = GetAssemblyType(assemblies[i].GetName().Name);
                if (assemblyTpe != null)
                {
                    assemblyTypes.Add((AssemblyTpe)assemblyTpe, assemblies[i].GetTypes());
                }
            }

            AddTypesFromAssembly(assemblyTypes[AssemblyTpe.AssemblyCSharp], types, interfaceType);
            AddTypesFromAssembly(assemblyTypes[AssemblyTpe.AssemblyCSharpFirstPass], types, interfaceType);

            return types;
        }

        private static void AddTypesFromAssembly(Type[] assemblyType, ICollection<Type> interfaceType, Type types)
        {
            if (assemblyType == null) return;

            for (var i = 0; i < assemblyType.Length; i++)
            {
                var type = assemblyType[i];
                if (type != types && types.IsAssignableFrom(type))
                {
                    interfaceType.Add(type);
                }
            }
        }
    }
}