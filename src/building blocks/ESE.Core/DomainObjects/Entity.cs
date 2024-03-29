﻿using ESE.Core.Messages;

namespace ESE.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        public void AddEvent(Event evento)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications?.Add(evento);
        }

        public void RemoveEvent(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }

        public void ClearEvent()
        {
            _notifications.Clear();
        }

        #region Comparações
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
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
        #endregion
    }
}
