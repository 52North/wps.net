using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    /// <summary>
    /// A collection containing _processOfferings.
    /// </summary>
    [Serializable]
    [XmlRoot("ProcessOfferings", Namespace = ModelNamespaces.Wps)]
    public class ProcessOfferingCollection : IList<ProcessOffering>
    {

        #region Model Properties

        /// <summary>
        /// An array containing the ProcessOffering/s.
        /// </summary>
        [XmlElement(ElementName = "ProcessOffering", Namespace = ModelNamespaces.Wps)]
        private List<ProcessOffering> _processOfferings = new List<ProcessOffering>();

        #endregion

        #region List Properties & Methods

        public IEnumerator<ProcessOffering> GetEnumerator()
        {
            return _processOfferings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ProcessOffering item)
        {
            _processOfferings.Add(item);
        }

        public void Clear()
        {
            _processOfferings.Clear();
        }

        public bool Contains(ProcessOffering item)
        {
            return _processOfferings.Contains(item);
        }

        public void CopyTo(ProcessOffering[] array, int arrayIndex)
        {
            _processOfferings.CopyTo(array, arrayIndex);
        }

        public bool Remove(ProcessOffering item)
        {
            return _processOfferings.Remove(item);
        }

        [XmlIgnore] public int Count => _processOfferings.Count;
        [XmlIgnore] public bool IsReadOnly => false;

        public int IndexOf(ProcessOffering item)
        {
            return _processOfferings.IndexOf(item);
        }

        public void Insert(int index, ProcessOffering item)
        {
            _processOfferings.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _processOfferings.RemoveAt(index);
        }

        public ProcessOffering this[int index]
        {
            get => _processOfferings[index];
            set => _processOfferings[index] = value;
        }

        #endregion

    }
}
