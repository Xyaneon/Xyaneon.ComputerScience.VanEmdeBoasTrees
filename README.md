# Xyaneon.ComputerScience.VanEmdeBoasTrees

[![NuGet](https://img.shields.io/nuget/v/Xyaneon.ComputerScience.VanEmdeBoasTrees.svg?style=flat)](https://www.nuget.org/packages/Xyaneon.ComputerScience.VanEmdeBoasTrees/)

A .NET Standard library which provides a van Emde Boas tree implementation for educational purposes.

## Usage

### Constructing a tree

Once you have the library added to your solution, you can create a van Emde Boas tree using code like the following:

```csharp
using Xyaneon.ComputerScience.VanEmdeBoasTrees;

// Snip...

var tree = new VanEmdeBoasTree(universe); // universe must be an int that is a power of two.
```

Besides being a value of two, note that the universe value you supply must also be larger than the maximum
value you plan on storing in the tree.

An alternate constructor is available if you also want to supply the values to store in the tree right away:

```csharp
IReadOnlyList<int> yourValues = new List<int>({ 1, 2, 3 }).AsReadOnly();

var tree = new vanEmdeBoasTree(4, yourValues);
```

### Tree operations

The [VanEmdeBoasTree][vanEmdeBoasTree] class implements the [IVanEmdeBoasTree][iVanEmdeBoasTree] interface.
Through this interface, you have access to the following standard van Emde Boas tree operations via its
methods and properties:

- Maximum
- Minimum
- Delete
- Insert
- Member
- Predecessor
- Successor

The maximum and minimum can be obtained in O(1) time; all the other operations take O(lg lg n) time, which is
a property of the van Emde Boas tree itself.

You can easily implement your own version of a van Emde Boas tree which implements the provided
[IVanEmdeBoasTree][iVanEmdeBoasTree] interface, then compare its results against those of the provided
[VanEmdeBoasTree][vanEmdeBoasTree] class to check its correctness using the exact same method calls.

All public code in this library is documented using XML documentation comments, which you can refer to for
further usage information. These comments should also be available to you through IntelliSense or the Visual
Studio Object Browser.

## Solution structure

This Visual Studio 2017 solution contains two projects.

### [Xyaneon.ComputerScience.VanEmdeBoasTrees][main-project]

A .NET Standard 2.0 library which provides an implementation of van Emde Boas trees.

### [Xyaneon.ComputerScience.VanEmdeBoasTrees.Test][test-project]

A .NET Core 2.1 unit testing project for Xyaneon.ComputerScience.VanEmdeBoasTrees
using the MSTest unit testing framework.

## License

This library is free and open-source software provided under the MIT license. See
the [LICENSE.txt][license] file for additional information.

## Additional information

This code was adapted from my submission for an MCS 5803: Intro to Algorithm Design homework
assignment at Lawrence Technological University during the Fall 2018 semester. The instructor,
Dr. David Fawcett, gave me permission to publicly post it as a resource for future students
who choose to use C#. I made a couple changes since the original submission, including adding
the [`IVanEmdeBoasTreeNode`][iVanEmdeBoasTreeNode] interface and adding more documentation.

The van Emde Boas tree implementation provided is based on the pseudocode presented in the
"Introduction to Algorithm Design" (CLRS) book.

This library is primarily intended for educational purposes on how van Emde Boas trees work.
It may be possible to create a more performant version of the code for use in actual
applications; this is left as an exercise to the reader.

## References

- [Wikipedia article on van Emde Boas trees][wikipedia-article]
- "Introduction to Algorithm Design" 3<sup>rd</sup> edition, by Thomas Cormen et al.
  ISBN: 978-0-262-03384-8. Available [here][book-page] from MIT Press. See chapter 20 for
  information specifically on van Emde Boas trees.

[vanEmdeBoasTree]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/blob/master/Xyaneon.ComputerScience.VanEmdeBoasTrees/VanEmdeBoasTree.cs
[iVanEmdeBoasTree]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/blob/master/Xyaneon.ComputerScience.VanEmdeBoasTrees/IVanEmdeBoasTree.cs
[main-project]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/tree/master/Xyaneon.ComputerScience.VanEmdeBoasTrees
[test-project]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/tree/master/Xyaneon.ComputerScience.VanEmdeBoasTrees.Test
[license]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/blob/master/LICENSE.txt
[iVanEmdeBoasTreeNode]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/blob/master/Xyaneon.ComputerScience.VanEmdeBoasTrees/IVanEmdeBoasTreeNode.cs
[wikipedia-article]: https://en.wikipedia.org/wiki/Van_Emde_Boas_tree
[book-page]: https://mitpress.mit.edu/books/introduction-algorithms
