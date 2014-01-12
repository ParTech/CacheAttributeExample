CacheAttribute Example
======================

Description
-----------
This example of how to implement caching on class methods without the need to modify any logic within the method.  

It works by adding a CacheAttribute to the class method that needs to be cached.  
Then Castle DynamicProxy is used to create a proxy class that intercepts any invocations of the class members and applies caching to the methods that implement the CacheAttribute.


References
------------
Blog: *under construction* -- http://www.partechit.nl/nl/blog/2014/01/implement-caching-on-instance-methods-with-a-custom-attribute  
GitHub: https://github.com/ParTech/CacheAttributeExample


Release notes
-------------
*1.0.0*
- Initial release


Author
------
This solution was brought to you and supported by Ruud van Falier, ParTech IT

Twitter: @BrruuD / @ParTechIT   
E-mail: ruud@partechit.nl   
Web: http://www.partechit.nl
