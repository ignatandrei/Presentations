# RSCG_TimeBombComment
Time Bomb comment for technical debt

Just add :

//TB: 2021-09-13 this is a comment transformed into an error

and you will see the error!

The general form is

//TB: yyyy-MM-dd whatever here


Also,you can have this on methods
[Obsolete("should be deleted", TB_20210915)]
static string Test1()
{
    return "asdasd";
}
