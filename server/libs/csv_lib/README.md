csv_lib
=====

A rebar plugin

Build
-----

    $ rebar3 compile

Use
---

Add the plugin to your rebar config:

    {plugins, [
        { csv_lib, ".*", {git, "git@host:user/csv_lib.git", {tag, "0.1.0"}}}
    ]}.

Then just call your plugin directly in an existing application:


    $ rebar3 csv_lib
    ===> Fetching csv_lib
    ===> Compiling csv_lib
    <Plugin Output>
