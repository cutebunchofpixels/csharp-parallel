namespace merge_sort;

public class MergeSort
{
    private const int MinimumSubarraySize = 5000;
    
    private static void Merge(int[] source, int[] left, int[] right)
    {
        int i = 0, j = 0, k = 0;

        while (i < left.Length && j < right.Length)
        {
            if (left[i] < right[j])
            {
                source[k] = left[i];
                i++;
            }
            else
            {
                source[k] = right[j];
                j++;
            }
            k++;
        }

        while (i < left.Length)
        {
            source[k] = left[i];
            i++;
            k++;
        }

        while (j < right.Length)
        {
            source[k] = right[j];
            j++;
            k++;
        }
    }

    public static void SortSerial(int[] array)
    {
        if (array.Length <= 1)
        {
            return;
        }

        var middle = array.Length / 2;
        var left = new int[middle];
        var right = new int[array.Length - middle];

        Array.Copy(array, 0, left, 0, left.Length);
        Array.Copy(array, middle, right, 0, right.Length);

        SortSerial(left);
        SortSerial(right);

        Merge(array, left, right);
    }

    public static void SortParallel(int[] array, int depth)
    {
        if (array.Length <= MinimumSubarraySize)
        {
            SortSerial(array);
            return;
        }
        
        var middle = array.Length / 2;
        var left = new int[middle];
        var right = new int[array.Length - middle];

        Array.Copy(array, 0, left, 0, left.Length);
        Array.Copy(array, middle, right, 0, right.Length);
        
        if (depth < Environment.ProcessorCount)
        {
            Parallel.Invoke(
                () => SortParallel(left, depth + 1),
                () => SortParallel(right, depth + 1));
        }
        else
        {
            SortParallel(left, depth);
            SortParallel(right, depth);
        }
        
        Merge(array, left, right);
    }
}