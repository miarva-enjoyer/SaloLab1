Application app = new Application(new LightFactory());
app.Run();
app=new Application(new DarkFactory());
app.Run();

abstract class GUIFactory
{
    public abstract AbstractButton CreateButton();
    public abstract AbstractCheckbox CreateCheckbox();
}

class LightFactory : GUIFactory
{
    public override AbstractButton CreateButton()
    {
        return new ButtonLight();
    }

    public override AbstractCheckbox CreateCheckbox()
    {
        return new CheckboxLight();
    }
}

class DarkFactory : GUIFactory
{
    public override AbstractButton CreateButton()
    {
        return new ButtonDark();
    }

    public override AbstractCheckbox CreateCheckbox()
    {
        return new CheckboxDark();
    }
}

abstract class AbstractButton
{
    public abstract void Render();
}

abstract class AbstractCheckbox
{
    public abstract void Render();
}

class ButtonLight : AbstractButton
{
    public override void Render()
    {
        Console.WriteLine("Rendered light button");
    }
}

class CheckboxLight : AbstractCheckbox
{
    public override void Render()
    {
        Console.WriteLine("Rendered light checkbox");
    }
}

class ButtonDark : AbstractButton
{
    public override void Render()
    {
        Console.WriteLine("Rendered dark button");
    }
}

class CheckboxDark : AbstractCheckbox
{
    public override void Render()
    {
        Console.WriteLine("Rendered dark checkbox");
    }
}

class Application
{
    private AbstractButton AbstractButton;
    private AbstractCheckbox AbstractCheckbox;

    public Application(GUIFactory factory)
    {
        AbstractButton = factory.CreateButton();
        AbstractCheckbox = factory.CreateCheckbox();
    }
    public void Run()
    {
        AbstractButton.Render();
        AbstractCheckbox.Render();
    }
}

