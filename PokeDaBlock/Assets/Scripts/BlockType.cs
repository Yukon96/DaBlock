using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockType  
{
    // Properties
    public string Name { get; private set; }
    public HashSet<BlockType> Immune { get; private set; } = new();
    public HashSet<BlockType> WeakTo { get; private set; } = new();
    public HashSet<BlockType> ResistantTo { get; private set; } = new();
    public HashSet<BlockType> SuperEffectiveAgainst { get; private set; } = new();

    // Static registry
    private static readonly Dictionary<string, BlockType> AllTypes = new();

    // Constructor
    private BlockType(string name) => Name = name;

    // Static Accessors
    public static BlockType GetOrCreateType(string name)
    {
        if (!AllTypes.TryGetValue(name, out var type))
        {
            type = new BlockType(name);
            AllTypes[name] = type;
        }
        return type;
    }

    public static IEnumerable<BlockType> GetAllTypes() => AllTypes.Values;

    // Methods to Add Relationships
    public BlockType AddImmunities(params BlockType[] types) { Immune.UnionWith(types); return this; }
    public BlockType AddWeaknesses(params BlockType[] types) { WeakTo.UnionWith(types); return this; }
    public BlockType AddResistances(params BlockType[] types) { ResistantTo.UnionWith(types); return this; }
    public BlockType AddSuperEffectiveAgainst(params BlockType[] types) { SuperEffectiveAgainst.UnionWith(types); return this; }

    // Combine Two Types
    public static BlockType Combine(BlockType type1, BlockType type2)
    {
        var combined = new BlockType($"{type1.Name}/{type2.Name}");

        combined.Immune.UnionWith(type1.Immune.Union(type2.Immune));
        combined.WeakTo.UnionWith(type1.WeakTo.Union(type2.WeakTo).Except(type1.Immune).Except(type2.Immune));
        combined.ResistantTo.UnionWith(type1.ResistantTo.Union(type2.ResistantTo).Except(type1.WeakTo).Except(type2.WeakTo));
        combined.SuperEffectiveAgainst.UnionWith(type1.SuperEffectiveAgainst.Union(type2.SuperEffectiveAgainst));

        return combined;
    }

    // Query Super Effective Coverage
    public static IEnumerable<BlockType> GetSuperEffectiveCoverage(params BlockType[] types) =>
        types.SelectMany(t => t.SuperEffectiveAgainst).Distinct();

    // String Representation
    public override string ToString() => Name;

    // Initialize Common Types and Relationships
    public static void InitializeTypes()
    {
        // Define types
        var bug = GetOrCreateType("Bug");
        var dark = GetOrCreateType("Dark");
        var dragon = GetOrCreateType("Dragon");
        var electric = GetOrCreateType("Electric");
        var fairy = GetOrCreateType("Fairy");
        var fighting = GetOrCreateType("Fighting");
        var fire = GetOrCreateType("Fire");
        var flying = GetOrCreateType("Flying");
        var ghost = GetOrCreateType("Ghost");
        var grass = GetOrCreateType("Grass");
        var ground = GetOrCreateType("Ground");
        var ice = GetOrCreateType("Ice");
        var normal = GetOrCreateType("Normal");
        var poison = GetOrCreateType("Poison");
        var psychic = GetOrCreateType("Psychic");
        var rock = GetOrCreateType("Rock");
        var steel = GetOrCreateType("Steel");
        var water = GetOrCreateType("Water");

        // Relationships
        bug.AddWeaknesses(flying, rock, fire)
           .AddResistances(fighting, ground, grass)
           .AddSuperEffectiveAgainst(grass, psychic, dark);

        dark.AddWeaknesses(fighting, bug, fairy)
            .AddResistances(ghost, dark)
            .AddSuperEffectiveAgainst(ghost, psychic);

        dragon.AddWeaknesses(ice, dragon, fairy)
              .AddResistances(fire, water, grass, electric)
              .AddSuperEffectiveAgainst(dragon);

        electric.AddWeaknesses(ground)
                .AddResistances(flying, steel, electric)
                .AddSuperEffectiveAgainst(flying, water);

        fairy.AddWeaknesses(poison, steel)
             .AddResistances(fighting, bug, dark)
             .AddSuperEffectiveAgainst(fighting, dragon, dark);

        fighting.AddWeaknesses(flying, psychic, fairy)
                .AddResistances(bug, rock, dark)
                .AddSuperEffectiveAgainst(normal, ice, rock, dark, steel);

        fire.AddWeaknesses(water, rock, ground)
            .AddResistances(bug, steel, fire, grass, ice, fairy)
            .AddSuperEffectiveAgainst(bug, steel, grass, ice);

        flying.AddWeaknesses(rock, electric, ice)
              .AddResistances(fighting, bug, grass)
              .AddSuperEffectiveAgainst(bug, fighting, grass);

        ghost.AddWeaknesses(ghost, dark)
             .AddImmunities(normal, fighting)
             .AddResistances(poison, bug)
             .AddSuperEffectiveAgainst(ghost, psychic);

        grass.AddWeaknesses(flying, poison, bug, fire, ice)
             .AddResistances(ground, water, grass, electric)
             .AddSuperEffectiveAgainst(ground, rock, water);

        ground.AddWeaknesses(water, grass, ice)
              .AddImmunities(electric)
              .AddResistances(poison, rock)
              .AddSuperEffectiveAgainst(poison, rock, steel, fire, electric);

        ice.AddWeaknesses(fighting, rock, steel, fire)
           .AddResistances(ice)
           .AddSuperEffectiveAgainst(grass, ground, flying, dragon);

        normal.AddWeaknesses(fighting)
              .AddImmunities(ghost)
              .AddSuperEffectiveAgainst(); // No super effective types

        poison.AddWeaknesses(ground, psychic)
              .AddResistances(fighting, poison, bug, grass, fairy)
              .AddSuperEffectiveAgainst(grass, fairy);

        psychic.AddWeaknesses(bug, ghost, dark)
               .AddResistances(fighting, psychic)
               .AddSuperEffectiveAgainst(fighting, poison);

        rock.AddWeaknesses(fighting, ground, steel, water, grass)
            .AddResistances(normal, flying, poison, fire)
            .AddSuperEffectiveAgainst(flying, bug, fire, ice);

        steel.AddWeaknesses(fighting, ground, fire)
             .AddImmunities(poison)
             .AddResistances(normal, flying, bug, steel, grass, psychic, ice, dragon, fairy)
             .AddSuperEffectiveAgainst(rock, ice, fairy);

        water.AddWeaknesses(grass, electric)
             .AddResistances(steel, fire, water, ice)
             .AddSuperEffectiveAgainst(ground, rock, fire);
    }

}
