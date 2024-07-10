using System;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu(menuName = "MiJuego/AdministradorDePrefabs")]
public class AdministradorDePrefabs : ScriptableObject
{
    public GameObject prefab;
    public string descripcion = "Este es un hueso";
}

#if UNITY_EDITOR
[CustomEditor(typeof(AdministradorDePrefabs))]
public class AdministradorDePrefabsSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.HelpBox("Si mueves este archivo a otro lugar, también cambia la ruta en UiLibraryMenus!", MessageType.Info);
    }
}
#endif
public class UiLibraryMenus
{
    private const string RutaAdministradorDePrefabs = "Assets/_Juego/ScriptableObjects/EditorExtensions/AdministradorDePrefabs.asset";

    private static AdministradorDePrefabs LocalizarAdministradorDePrefabs() => AssetDatabase.LoadAssetAtPath<AdministradorDePrefabs>(RutaAdministradorDePrefabs);

    private static void InstanciarSeguro(Func<AdministradorDePrefabs, GameObject> selectorDeItem)
    {
        var administradorDePrefabs = LocalizarAdministradorDePrefabs();

        if (!administradorDePrefabs)
        {
            Debug.LogWarning($"AdministradorDePrefabs no encontrado en la ruta {RutaAdministradorDePrefabs}");
            return;
        }

        var item = selectorDeItem(administradorDePrefabs);
        var Instantiate = PrefabUtility.InstantiatePrefab(item, Selection.activeTransform);

        Undo.RegisterCreatedObjectUndo(Instantiate, $"Crear {Instantiate.name}");
        Selection.activeObject = Instantiate;

        // Mostrar el menú con información
        MostrarMenu((GameObject)Instantiate, administradorDePrefabs);

        GameObject instantiatedGameObject = (GameObject)PrefabUtility.InstantiatePrefab(item, Selection.activeTransform);

        // Ahora pasa el GameObject a InstanciarSeguro
        InstanciarSeguro(administradorDePrefabs => instantiatedGameObject);
    }

    private static void MostrarMenu(GameObject Instantiate, AdministradorDePrefabs administradorDePrefabs)
    {
        // Crear un menú con información sobre el prefab
        string textoMenu = $"Este es un {administradorDePrefabs.descripcion}";
        Debug.Log(textoMenu);
        
    }

    [MenuItem("GameObject/MiJuego/UI/Botones/Boton", priority = 100)]
    private static void CrearBoton()
    {
        InstanciarSeguro(administradorDePrefabs => administradorDePrefabs.prefab);
    }
}