var HandleIO = {
    WindowAlert : function(_message)
    {
        window.alert(Pointer_stringify(_message));
    },
    SyncFiles : function()
    {
        FS.syncfs(false,function (_error) 
        {
        });
    }
};

mergeInto(LibraryManager.library, HandleIO);