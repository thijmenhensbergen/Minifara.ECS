using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinafaraECS
{
    public class Entity
    {
        public Entity? Parent;
        public Reality? Reality;
        public string Name = "";
        private List<Entity> Children = [];
        private List<Component> Components = [];

        public void AddChild(Entity Child)
        {
            Child.Parent = this;
            Children.Add(Child);
            foreach (var Component in Child.Components)
            {
                Component.OnBegin(); 
            }
        }
        public void AddChildren(List<Entity> NewChildren)
        {
            foreach (var NewChild in NewChildren)
            {
                AddChild(NewChild);
            }
        }
        public void RemoveChild(string EntityName)
        {
            Entity? Child = Children.FirstOrDefault(e => e.Name == EntityName);
            if (Child != null)
            {
                Children.Remove(Child);
            }
        }
        public void RemoveChildren(string EntityName, bool CaseSensitive = true)
        {
            foreach (var Child in Children)
            {
                if (CaseSensitive)
                {
                    if (Child.Name.Contains(EntityName))
                    {
                        Children.Remove(Child);
                    }
                } else {
                    if (Child.Name.Contains(EntityName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        Children.Remove(Child);
                    }
                }

            }
        }
        public void AddComponent(Component NewComponent)
        {
            Components.Add(NewComponent);
            NewComponent.OnBegin();
        }

        private void ProcessComponents(double DeltaTime)
        {
            foreach (var component in Components)
            {
                component.Process(DeltaTime);
            }
            foreach (var child in Children)
            {
                child.ProcessComponents(DeltaTime);
            }
        }
    }
}
