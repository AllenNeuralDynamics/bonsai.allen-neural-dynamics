using Bonsai.Design;
using Bonsai.Expressions;
using System;
using System.Collections.Generic;
using System.Drawing;

public class PropertyGridVisualizer : DialogTypeVisualizer
{
    PropertyGrid control;

    public override void Load(IServiceProvider provider)
    {

        var nestedWorkflowBuilder = (ExpressionBuilderGraph)provider.GetService(typeof(ExpressionBuilderGraph)); //gets the whole workflow
        var context = (ITypeVisualizerContext)provider.GetService(typeof(ITypeVisualizerContext));
        var visualizerElement = ExpressionBuilder.GetVisualizerElement(context.Source).Builder; // get the class reference that originated the visualizer
        var visualizerElementContext = GetContextByElement(nestedWorkflowBuilder, visualizerElement);
        if (visualizerElement is null)
        {
            throw new NullReferenceException("Could not find the reference to the target object in the workflow.");
        }

        control = new PropertyGrid();
        control.Font = new Font(control.Font.FontFamily, 16.2F);
        control.Dock = System.Windows.Forms.DockStyle.Fill;
        control.SelectedObject = visualizerElementContext;
        control.Size = new Size(400, 450);

        var visualizerService = (IDialogTypeVisualizerService)provider.GetService(typeof(IDialogTypeVisualizerService));
        if (visualizerService != null)
        {
            visualizerService.AddControl(control);
        }
    }

    public override void Show(object value)
    {
    }

    public override void Unload()
    {
        if (control != null)
        {
            control.Dispose();
            control = null;
        }
    }

    static bool IsGroup(IWorkflowExpressionBuilder builder)
    {
        return builder is IncludeWorkflowBuilder || builder is GroupWorkflowBuilder;
    }

    static IEnumerable<ExpressionBuilder> SelectContextElements(ExpressionBuilderGraph source)
    {
        foreach (var node in source)
        {
            var element = ExpressionBuilder.Unwrap(node.Value);
            if (element is DisableBuilder) continue;
            yield return element;

            var workflowBuilder = element as IWorkflowExpressionBuilder;
            if (IsGroup(workflowBuilder))
            {
                var workflow = workflowBuilder.Workflow;
                if (workflow == null) continue;
                foreach (var groupElement in SelectContextElements(workflow))
                {
                    yield return groupElement;
                }
            }
        }
    }

    static ExpressionBuilderGraph GetContextByElement(ExpressionBuilderGraph sourceWorkflow, ExpressionBuilder targetElement)
    {
        foreach (var element in SelectContextElements(sourceWorkflow)) //Loop elements
        {
            if (element == targetElement){ //compare the reference to the target object
                return sourceWorkflow;
            }

            if (element is WorkflowExpressionBuilder workflowBuilder) // recursively attempt to find the element in the nested builders
            {
                return GetContextByElement(workflowBuilder.Workflow, targetElement);
            }
        }
        return null;
    }
}



