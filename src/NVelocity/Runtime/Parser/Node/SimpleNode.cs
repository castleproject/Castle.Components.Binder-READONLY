 /* Generated By:JJTree: Do not edit this line. SimpleNode.java */
/*
* The Apache Software License, Version 1.1
*
* Copyright (c) 2000-2001 The Apache Software Foundation.  All rights
* reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions
* are met:
*
* 1. Redistributions of source code must retain the above copyright
*    notice, this list of conditions and the following disclaimer.
*
* 2. Redistributions in binary form must reproduce the above copyright
*    notice, this list of conditions and the following disclaimer in
*    the documentation and/or other materials provided with the
*    distribution.
*
* 3. The end-user documentation included with the redistribution, if
*    any, must include the following acknowlegement:
*       "This product includes software developed by the
*        Apache Software Foundation (http://www.apache.org/)."
*    Alternately, this acknowlegement may appear in the software itself,
*    if and wherever such third-party acknowlegements normally appear.
*
* 4. The names "The Jakarta Project", "Velocity", and "Apache Software
*    Foundation" must not be used to endorse or promote products derived
*    from this software without prior written permission. For written
*    permission, please contact apache@apache.org.
*
* 5. Products derived from this software may not be called "Apache"
*    nor may "Apache" appear in their names without prior written
*    permission of the Apache Group.
*
* THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED
* WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
* OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED.  IN NO EVENT SHALL THE APACHE SOFTWARE FOUNDATION OR
* ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
* SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
* LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF
* USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
* OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
* OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
* SUCH DAMAGE.
* ====================================================================
*
* This software consists of voluntary contributions made by many
* individuals on behalf of the Apache Software Foundation.  For more
* information on the Apache Software Foundation, please see
* <http://www.apache.org/>.
*/

namespace NVelocity.Runtime.Parser.Node
{
	using System;
	using System.IO;
	using System.Text;
	using NVelocity.Context;
	using NVelocity.Runtime.Exception;

	public class SimpleNode : INode
	{
		public Token FirstToken
		{
			get { return first; }

			set { this.first = value; }

		}

		public Token LastToken
		{
			get { return last; }

		}

		public int Type
		{
			get { return id; }

		}

		public int Info
		{
			get { return info; }

			set { this.info = value; }

		}

		public int Line
		{
			get { return first.beginLine; }

		}

		public int Column
		{
			get { return first.beginColumn; }

		}

		protected internal RuntimeServices rsvc = null;

		protected internal INode parent;
		protected internal INode[] children;
		protected internal int id;
		protected internal Parser parser;

		protected internal int info; // added
		public bool state;
		protected internal bool invalid = false;

		/* Added */
		protected internal Token first, last;

		public SimpleNode(int i)
		{
			id = i;
		}

		public SimpleNode(Parser p, int i) : this(i)
		{
			parser = p;
		}

		public void jjtOpen()
		{
			first = parser.getToken(1); // added
		}

		public void jjtClose()
		{
			last = parser.getToken(0); // added
		}


		public void jjtSetParent(INode n)
		{
			parent = n;
		}

		public INode jjtGetParent()
		{
			return parent;
		}

		public void jjtAddChild(INode n, int i)
		{
			if (children == null)
			{
				children = new INode[i + 1];
			}
			else if (i >= children.Length)
			{
				INode[] c = new INode[i + 1];
				Array.Copy(children, 0, c, 0, children.Length);
				children = c;
			}
			children[i] = n;
		}

		public INode jjtGetChild(int i)
		{
			return children[i];
		}

		public int jjtGetNumChildren()
		{
			return (children == null) ? 0 : children.Length;
		}

		/// <summary>Accept the visitor. *
		/// </summary>
		public virtual Object jjtAccept(ParserVisitor visitor, Object data)
		{
			return visitor.visit(this, data);
		}

		/// <summary>Accept the visitor. *
		/// </summary>
		public Object childrenAccept(ParserVisitor visitor, Object data)
		{
			if (children != null)
			{
				for (int i = 0; i < children.Length; ++i)
				{
					children[i].jjtAccept(visitor, data);
				}
			}
			return data;
		}

		/* You can override these two methods in subclasses of SimpleNode to
	    customize the way the node appears when the tree is dumped.  If
	    your output uses more than one line you should override
	    toString(String), otherwise overriding toString() is probably all
	    you need to do. */

		//    public String toString()
		// {
		//    return ParserTreeConstants.jjtNodeName[id];
		// }
		public String toString(String prefix)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.Object.toString' may return a different value. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1043"'
			return prefix + ToString();
		}

		/* Override this method if you want to customize how the node dumps
		out its children. */

		public void dump(String prefix)
		{
			Console.Out.WriteLine(toString(prefix));
			if (children != null)
			{
				for (int i = 0; i < children.Length; ++i)
				{
					SimpleNode n = (SimpleNode) children[i];
					if (n != null)
					{
						n.dump(prefix + " ");
					}
				}
			}
		}

		// All additional methods

		public virtual String literal()
		{
			Token t = first;
			StringBuilder sb = new StringBuilder(t.image);

			while (t != last)
			{
				t = t.next;
				sb.Append(t.image);
			}

			return sb.ToString();
		}

		public virtual Object init(InternalContextAdapter context, Object data)
		{
			/*
	    * hold onto the RuntimeServices
	    */

			rsvc = (RuntimeServices) data;

			int i, k = jjtGetNumChildren();

			for (i = 0; i < k; i++)
			{
				try
				{
					jjtGetChild(i).init(context, data);
				}
				catch (ReferenceException re)
				{
					rsvc.error(re);
				}
			}

			return data;
		}

		public virtual bool evaluate(InternalContextAdapter context)
		{
			return false;
		}

		public virtual Object Value(InternalContextAdapter context)
		{
			return null;
		}

		public virtual bool render(InternalContextAdapter context, TextWriter writer)
		{
			int i, k = jjtGetNumChildren();

			for (i = 0; i < k; i++)
				jjtGetChild(i).render(context, writer);

			return true;
		}

		public virtual Object execute(Object o, InternalContextAdapter context)
		{
			return null;
		}


		public void setInvalid()
		{
			invalid = true;
		}

		public bool isInvalid()
		{
			return invalid;
		}


	}
}