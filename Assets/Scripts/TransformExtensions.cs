using UnityEngine;

namespace UnityExtensions
{
	public static class TransformExtensions
	{
		public static void SetPositionX(this Transform t, float x)
		{
			t.position = new Vector3(x, t.position.y, t.position.z);
		}

		public static void SetLocalPositionX(this Transform t, float x)
		{
			t.localPosition = new Vector3(x, t.localPosition.y, t.localPosition.z);
		}

		public static void SetPositionY(this Transform t, float y)
		{
			t.position = new Vector3(t.position.x, y, t.position.z);
		}

		public static void SetLocalPositionY(this Transform t, float y)
		{
			t.localPosition = new Vector3(t.localPosition.x, y, t.localPosition.z);
		}

		public static void SetPositionZ(this Transform t, float z)
		{
			t.position = new Vector3(t.position.x, t.position.y, z);
		}

		public static void SetLocalPositionZ(this Transform t, float z)
		{
			t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, z);
		}

		public static void LerpPosition(
			this Transform t, Vector3 start, Vector3 end, float i)
		{
			t.position = Vector3.Lerp(start, end, i);
		}

		public static void LerpLocalPosition(
			this Transform t, Vector3 start, Vector3 end, float i)
		{
			t.localPosition = Vector3.Lerp(start, end, i);
		}

		public static void LerpPositionX(
			this Transform t, float start, float end, float i)
		{
			t.SetPositionX(Mathf.Lerp(start, end, i));
		}

		public static void LerpLocalPositionX(
			this Transform t, float start, float end, float i)
		{
			t.SetLocalPositionX(Mathf.Lerp(start, end, i));
		}

		public static void LerpPositionY(
			this Transform t, float start, float end, float i)
		{
			t.SetPositionY(Mathf.Lerp(start, end, i));
		}

		public static void LerpLocalPositionY(
			this Transform t, float start, float end, float i)
		{
			t.SetLocalPositionY(Mathf.Lerp(start, end, i));
		}

		public static void LerpPositionZ(
			this Transform t, float start, float end, float i)
		{
			t.SetPositionY(Mathf.Lerp(start, end, i));
		}

		public static void LerpLocalPositionZ(
			this Transform t, float start, float end, float i)
		{
			t.SetLocalPositionY(Mathf.Lerp(start, end, i));
		}
	}
}
