using UnityEditor;
using UnityEngine;

namespace Views
{

	[CustomEditor(typeof(View), true)]
	public class ViewInspector : Editor
	{

		override public void OnInspectorGUI()
		{
			View view = target as View;

			if(SceneView.lastActiveSceneView != null && GUILayout.Button("Focus"))
			{
				SceneView.lastActiveSceneView.FrameSelected();
			}

			if(Application.isPlaying)
			{
				GUILayout.BeginHorizontal();
				if(GUILayout.Button("Show")) view.Show(false);
				if(GUILayout.Button("Hide")) view.Hide(false);
				GUILayout.EndHorizontal();
			}

			DrawDefaultInspector();
		}

	}

}