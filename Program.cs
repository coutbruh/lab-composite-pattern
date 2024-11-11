
using System;
using System.Collections.Generic;

// 1. Интерфейс IDocumentComponent
public interface IDocumentComponent
{
    void Add(IDocumentComponent component);
    void Remove(IDocumentComponent component);
    void Display(int indent = 0);
}

// 2. Класс Paragraph - представляет простой текст
public class Paragraph : IDocumentComponent
{
    private readonly string _text;

    public Paragraph(string text)
    {
        _text = text;
    }

    public void Add(IDocumentComponent component)
    {
        throw new InvalidOperationException("Нельзя добавить компоненты в параграф.");
    }

    public void Remove(IDocumentComponent component)
    {
        throw new InvalidOperationException("Нельзя удалить компоненты из параграфа.");
    }

    public void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + _text);
    }
}

// 2. Класс Section - имеет заголовок и список компонентов (параграфов и разделов)
public class Section : IDocumentComponent
{
    private readonly string _title;
    private readonly List<IDocumentComponent> _components = new List<IDocumentComponent>();

    public Section(string title)
    {
        _title = title;
    }

    public void Add(IDocumentComponent component)
    {
        _components.Add(component);
    }

    public void Remove(IDocumentComponent component)
    {
        _components.Remove(component);
    }

    public void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + "Раздел: " + _title);
        foreach (var component in _components)
        {
            component.Display(indent + 2);
        }
    }
}

// 2. Класс Document - содержит разделы и методы управления ими
public class Document : IDocumentComponent
{
    private readonly string _title;
    private readonly List<IDocumentComponent> _sections = new List<IDocumentComponent>();

    public Document(string title)
    {
        _title = title;
    }

    public void Add(IDocumentComponent component)
    {
        _sections.Add(component);
    }

    public void Remove(IDocumentComponent component)
    {
        _sections.Remove(component);
    }

    public void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + "Документ: " + _title);
        foreach (var section in _sections)
        {
            section.Display(indent + 2);
        }
    }
}

public class Program
{
    public static void Main()
    {
        // Создание документа
        Document document = new Document("Документ Компании");

        // Создание разделов и параграфов
        Section section1 = new Section("Введение");
        section1.Add(new Paragraph("Это параграф введения."));

        Section section2 = new Section("Основной Раздел");
        section2.Add(new Paragraph("Это первый параграф основного раздела."));

        Section subSection = new Section("Подраздел");
        subSection.Add(new Paragraph("Это параграф в подразделе."));

        section2.Add(subSection);  // Вложенный раздел

        document.Add(section1);
        document.Add(section2);

        document.Display();
    }
}
