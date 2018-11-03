# Xyaneon.ComputerScience.VanEmdeBoasTrees

A .NET Standard library which provides a van Emde Boas tree implementation for educational purposes.

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


[main-project]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/tree/master/Xyaneon.ComputerScience.VanEmdeBoasTrees
[test-project]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/tree/master/Xyaneon.ComputerScience.VanEmdeBoasTrees.Test
[license]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/blob/master/LICENSE.txt
[iVanEmdeBoasTreeNode]: https://github.com/Xyaneon/Xyaneon.ComputerScience.VanEmdeBoasTrees/blob/master/Xyaneon.ComputerScience.VanEmdeBoasTrees/IVanEmdeBoasTreeNode.cs
[wikipedia-article]: https://en.wikipedia.org/wiki/Van_Emde_Boas_tree
[book-page]: https://mitpress.mit.edu/books/introduction-algorithms
