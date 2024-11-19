# Model Hyperparameter Testing and Evaluation

In this document, we outline the experiments conducted to evaluate the performance of a neural network model by testing different hyperparameters. The goal is to assess how varying the learning rate, number of epochs, and batch size affects the model's accuracy and loss.

## 1. Hyperparameters Tested

The following hyperparameters were tested in our experiments:

### Learning Rate
- **Description**: The learning rate determines the size of the steps the optimizer takes during training. A small learning rate might slow down the training, while a large learning rate could result in overshooting the optimal solution.
- **Values tested**: `0.1`, `0.01`, `0.001`

### Epochs
- **Description**: The number of epochs indicates how many times the model will go through the entire training dataset.
- **Values tested**: `10`, `20`, `50`

### Batch Size
- **Description**: The batch size controls how many samples are processed before updating the model weights.
- **Values tested**: `32`, `64`, `128`

## 2. Experiment Setup

For each combination of these hyperparameters, the model was trained and evaluated. The following metrics were recorded for each experiment:

- **Training accuracy** and **validation accuracy**
- **Training loss** and **validation loss**

Each experiment was repeated to ensure robustness and to avoid overfitting or random fluctuations in performance. 

## 3. Results Overview

The results of the experiments are summarized in the following sections. For each combination of hyperparameters, the following visualizations were generated:

1. **Training and Validation Loss/Accuracy Graph**: A plot showing the training and validation loss/accuracy over epochs.
2. **Predictions Grid**: A grid of images showing the predicted labels versus the true labels for a subset of test images.

## 4. Example Test Results

### Test 1: Learning Rate = 0.1, Epochs = 10, Batch Size = 32

> This test explores the impact of a **high learning rate** (0.1) on training with **few epochs** (10) and a **small batch size** (32). The aim is to evaluate if the model can converge quickly with such an aggressive learning rate and limited epochs, but also to see if it might lead to instability or suboptimal performance due to the large update steps in each iteration.

- **Training Accuracy**: 85.2%
- **Validation Accuracy**: 82.5%
- **Training Loss**: 0.50
- **Validation Loss**: 0.60

#### Training and Validation Accuracy Plot:
![Training and Validation Accuracy](assets/graph1.png)

#### Predicted vs True Labels:
![Predictions Grid](assets/grid1.png)

#### Conclusion

Ha tardado mucho en entrenar? Es accurated? etc

---

### Test 2: Learning Rate = 0.01, Epochs = 50, Batch Size = 64

> This test examines the behavior of the model with a **moderate learning rate** (0.01) and a **larger number of epochs** (50) for training. A **medium batch size** (64) is used to balance training speed and stability. The objective is to see if a smaller learning rate combined with more epochs helps the model converge more smoothly and reach higher accuracy without overfitting.

- **Training Accuracy**: 92.0%
- **Validation Accuracy**: 89.5%
- **Training Loss**: 0.25
- **Validation Loss**: 0.38

#### Training and Validation Accuracy Plot:
![Training and Validation Accuracy](assets/graph2.png)

#### Predicted vs True Labels:
![Predictions Grid](assets/grid2.png)

#### Conclusion

Ha tardado mucho en entrenar? Es accurated? etc

---

### Test 3: Learning Rate = 0.001, Epochs = 50, Batch Size = 128

> In this test, the **learning rate** is set to a **very low value** (0.001) and the number of **epochs** is set to 50, with a **large batch size** (128). The goal is to investigate whether a smaller learning rate allows for a more gradual and stable convergence and whether a larger batch size speeds up training without compromising the model's ability to generalize. This configuration aims to find the optimal trade-off between stable learning and training efficiency.

- **Training Accuracy**: 93.5%
- **Validation Accuracy**: 90.2%
- **Training Loss**: 0.22
- **Validation Loss**: 0.30

#### Training and Validation Accuracy Plot:
![Training and Validation Accuracy](assets/graph3.png)

#### Predicted vs True Labels:
![Predictions Grid](assets/grid3.png)

#### Conclusion

Ha tardado mucho en entrenar? Es accurated? etc

---

## 5. Conclusion
