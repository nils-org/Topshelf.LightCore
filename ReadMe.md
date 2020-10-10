Topshelf.LightCore
================

![Logo](res/Logo-72.png)

[LightCore](https://github.com/JuergenGutsch/LightCore) bindings for [Topshelf](http://topshelf-project.com/)

Howto
-----
    
    // setup LightCore
    var builder = new ContainerBuilder();
    /* some fancy setup here */
    var container = builder.Build();

	// setup Topshelf
    var host = HostFactory.Run(x =>
    {
        x.UseLightCore(container); // Enable LightCore
        x.Service<SomeSevice>(s =>
        {
            s.ConstructUsingLightCore(); // Construct SomeSevice using LightCore
            s.WhenStarted(tc => tc.Start());
            s.WhenStopped(tc => tc.Stop());
            /* more Topshelf code... */
        });
    });
