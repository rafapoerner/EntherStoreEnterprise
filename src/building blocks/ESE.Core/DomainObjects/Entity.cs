using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ESE.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if(ReferenceEquals(this, compareTo)) return true;
            if(ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if(ReferenceEquals(a, null) &&  ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }


        // Cada instância de um objeto tem um hashcode(Cód aleatório)
        // Para não ter a chance de o cód de hash ser igual, multiplica o hash por 907 e concatenar com o hashCode do Id.
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        // Escreve o nome da entidade e o ID quando for colocar o .ToString()
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

    }
}
