YuShan(玉山) is a simple web framework based on OWIN for .net world.  It is still developing.  Please refer to development branch for source code at this point.
The framework is inspired by express and sinatra


There is a SIMPLE performance test report.  If you are interested, please refer to doc file for more detail.

|              Framework             	| How many requests can be handled per Second? (higher is better) 	|  Language  	|   Platform  	|
|:----------------------------------:	|:---------------------------------------------------------------:	|:----------:	|:-----------:	|
| martini                            	|                                                            8544 	|   Golang   	| Native Code 	|
| YuShan (玉山)                        	|                                                            6909 	|     C#     	|     CLR     	|
| YuShan (玉山) with Razor View Engine 	|                                                            6747 	|     C#     	|     CLR     	|
| ExpressJS                          	|                                                            6272 	| JavaScript 	|   Node.js   	|
| ExpressJS with EJS View Engine     	|                                                            2501 	| JavaScript 	|   Node.js   	|
| ExpressJS with Dust View Engine    	|                                                             632 	| JavaScript 	|   Node.js   	|
| ExpressJS with Jade View Engine    	|                                                             438 	| JavaScript 	|   Node.js   	|