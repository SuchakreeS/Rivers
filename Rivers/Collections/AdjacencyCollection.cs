﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Rivers.Collections
{
    /// <summary>
    /// Represents the base for collection of edges either originating, or targeting a node in a graph.
    /// </summary>
    public abstract class AdjacencyCollection : ICollection<Edge>
    {
        protected AdjacencyCollection(Node origin)
        {
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
        }
        
        /// <summary>
        /// Gets the node the adjacent edge collection is associated to.
        /// </summary>
        public Node Origin
        {
            get;
        }

        /// <inheritdoc />
        public abstract int Count
        {
            get;
        }

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets an edge based on the name of a neighbour.
        /// </summary>
        /// <param name="neighbourName">The name of the neighbour.</param>
        public abstract Edge this[string neighbourName]
        {
            get;
        }

        /// <summary>
        /// Gets an edge based on the neighbour.
        /// </summary>
        /// <param name="neighbour">The neighbour node.</param>
        public abstract Edge this[Node neighbour]
        {
            get;
        }

        /// <summary>
        /// Tries to get an edge based on the name of a neighbour.
        /// </summary>
        /// <param name="neighbourName">The name of the neighbour.</param>
        /// <param name="edge">The edge either originating, or going to the provided neighbour.</param>
        /// <returns>True if the edge is present, false otherwise.</returns>
        public abstract bool TryGetEdge(string neighbourName, out Edge edge);
        
        /// <summary>
        /// Tries to get an edge based on the neighbour.
        /// </summary>
        /// <param name="neighbour">The the neighbour.</param>
        /// <param name="edge">The edge either originating, or going to the provided neighbour.</param>
        /// <returns>True if the edge is present, false otherwise.</returns>
        public abstract bool TryGetEdge(Node neighbour, out Edge edge);

        /// <summary>
        /// Adds a new edge to the node, either originating or towards a new neighbour, depending on the value of <see cref="Outgoing"/>.
        /// </summary>
        /// <param name="neighbourName">The name of the neighbour.</param>
        public virtual void Add(string neighbourName)
        {
            Add(Origin.ParentGraph.Nodes[neighbourName]);
        }

        /// <summary>
        /// Adds a new edge to the node, either originating or towards a new neighbour, depending on the value of <see cref="Outgoing"/>.
        /// </summary>
        /// <param name="neighbour">The new neighbour.</param>
        public abstract void Add(Node neighbour);
        
        /// <inheritdoc />
        public abstract void Add(Edge edge);
        
        /// <inheritdoc cref="ICollection{T}.Clear()" />
        public abstract void Clear();

        /// <summary>
        /// Determines whether there exists an edge from/to the given node.  
        /// </summary>
        /// <param name="neighbourName">The name of the neighbour.</param>
        /// <returns>True if there exists an edge, false otherwise.</returns>
        public virtual bool Contains(string neighbourName)
        {
            return TryGetEdge(neighbourName, out _);
        }

        /// <summary>
        /// Determines whether there exists an edge from/to the given node.   
        /// </summary>
        /// <param name="neighbour">The neighbour.</param>
        /// <returns>True if there exists an edge, false otherwise.</returns>
        public virtual bool Contains(Node neighbour)
        {
            return TryGetEdge(neighbour, out _);
        }
        
        /// <inheritdoc />
        public abstract bool Contains(Edge edge);
        
        /// <inheritdoc />
        public abstract void CopyTo(Edge[] array, int arrayIndex);

        /// <summary>
        /// Tries to remove an edge to the given neighbour.
        /// </summary>
        /// <param name="neighbourName">The name of the neighbour.</param>
        /// <returns>True if it succeeded, false otherwise.</returns>
        public virtual bool Remove(string neighbourName)
        {
            return Contains(neighbourName) && Remove(Origin.ParentGraph.Nodes[neighbourName]);
        }

        /// <summary>
        /// Tries to remove an edge to the given neighbour.
        /// </summary>
        /// <param name="neighbour">The neighbour.</param>
        /// <returns>True if it succeeded, false otherwise.</returns>
        public abstract bool Remove(Node neighbour);
        
        /// <inheritdoc />
        public abstract bool Remove(Edge edge);

        /// <inheritdoc />
        public abstract IEnumerator<Edge> GetEnumerator();
        
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}